# Example Windows Power Shell script for downloading transaction report
$MERCHANT_ID = ""
$COLUMN = "timeOfRecord,order.id,transaction.id,sourceOfFunds.provided.card.number,sourceOfFunds.provided.card.expiry.month,sourceOfFunds.provided.card.expiry.year,transaction.amount,transaction.currency,transaction.type,transaction.acquirer.id,response.acquirerCode"
$HEADER = "time,order id,transaction id,card number,card expiry month,card expiry year,amount,currency,type,acquirer,acquirerCode"

# hostname of reporting/history download service
$SERVERHOST = ""

Add-Type -AssemblyName System.Web

if ( $SERVERHOST.Length -eq 0 )
{
    Write-Host "SERVERHOST variable must be set to reporting/history server name"
    exit 1
}

if ( $MERCHANT_ID.Length -eq 0 )
{
    Write-Host "MERCHANT_ID variable must be set to merchant profile id"
    exit 1
}

# initial date to be used, after first run, the last end date is stored and used instead
$START_DATE = "2012-01-01T00:00:00Z"
# set end date to now
$END_DATE = [DateTime]::Now.ToUniversalTime().ToString( "yyyy-MM-ddTHH:mm:ssZ" )

# report output filename
$OUTPUT_FILENAME = "$HOME\transactions.csv"

# file used to store last end date
$LAST_RUN_FILENAME = "$HOME\reportingLastRunTime"

$REPORTING_PASSWORD_FILE = "$HOME\reportingPassword"

# remind user to create the password file
if(!(Test-Path $REPORTING_PASSWORD_FILE))
{
    Write-host "The $REPORTING_PASSWORD_FILE file must exist, please create it with setpassword.ps1"
    exit 1
}

# update start date from last run's end date
if((Test-Path $LAST_RUN_FILENAME))
{
    $START_DATE = Get-Content $LAST_RUN_FILENAME
}

# decrypt the reporting password
$sspassword = get-content $REPORTING_PASSWORD_FILE  | ConvertTo-SecureString
$Ptr = [System.Runtime.InteropServices.Marshal]::SecureStringToCoTaskMemUnicode($sspassword)
$PASSWORD = [System.Runtime.InteropServices.Marshal]::PtrToStringUni($Ptr)
[System.Runtime.InteropServices.Marshal]::ZeroFreeCoTaskMemUnicode($Ptr)

# set gzip compression
try
{
    # test if class already defined
    $webclientTest = New-Object MyWebClient
}
catch [System.Management.Automation.PSArgumentException]
{
    Add-Type -ReferencedAssemblies "System.Net" -TypeDefinition @"
        using System.Net;
        public class MyWebClient : WebClient
        {
            protected override WebRequest GetWebRequest(System.Uri webUrl)
            {
                HttpWebRequest retVal = base.GetWebRequest(webUrl) as HttpWebRequest;
                retVal.AutomaticDecompression = DecompressionMethods.GZip;
                return retVal;
            }
			
			public void avoidWarning() {}
        }
"@
}

function DownloadFile
{
    $FULL_URL="https://$SERVERHOST/history/version/1/merchant/$MERCHANT_ID/transaction"
	$FULL_URL = $FULL_URL +"?timeOfRecord.start=" + [System.Web.HttpUtility]::UrlEncode($START_DATE)
	$FULL_URL = $FULL_URL +"&timeOfRecord.end=" + [System.Web.HttpUtility]::UrlEncode($END_DATE)
	$FULL_URL = $FULL_URL +"&columns=" + [System.Web.HttpUtility]::UrlEncode($COLUMN)
	$FULL_URL = $FULL_URL +"&columnHeaders=" + [System.Web.HttpUtility]::UrlEncode($HEADER)
    # configure the http client
    $webclient = New-Object MyWebClient
    $webclient.Credentials = New-Object System.Net.NetworkCredential -ArgumentList "merchant.default", $PASSWORD
    $webclient.DownloadFile($FULL_URL,$OUTPUT_FILENAME)
}

try
{
    DownloadFile
}
catch [System.Net.WebException]
{
    if(!$_.Exception.Response)
    {
        Write-host $_.Exception
    }
    else
    {
        # get the actual the text recieved from server
        $errorResponse = (New-Object System.IO.StreamReader($_.Exception.Response.GetResponseStream(), [System.Text.Encoding]::GetEncoding("utf-8"))).ReadToEnd()
        if($errorResponse.contains("DATA_AVAILABLE_AFTER "))
        {
            $END_DATE = $errorResponse.substring(21,20)
            DownloadFile
        }
        else
        {
            Write-host $errorResponse
            exit 2
        }
    }
}

# store end date for re-use in next run
if(Test-Path $OUTPUT_FILENAME)
{
    $END_DATE | set-content $LAST_RUN_FILENAME
}