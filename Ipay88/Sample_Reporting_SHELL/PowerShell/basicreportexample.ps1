# Example Windows Power Shell script for downloading transaction report
$COLUMN = "timeOfRecord,order.id,transaction.id,sourceOfFunds.provided.card.number,sourceOfFunds.provided.card.expiry.month,sourceOfFunds.provided.card.expiry.year,transaction.amount,transaction.currency,transaction.type,transaction.acquirer.id,response.acquirerCode"
$HEADER = "time,order id,transaction id,card number,card expiry month,card expiry year,amount,currency,type,acquirer,acquirerCode"

# hostname of reporting/history download service
$SERVERHOST = ""

if ( $SERVERHOST.Length -eq 0 )
{
    Write-Host "SERVERHOST variable must be set to reporting/history server name"
    exit 1
}

if ( $ARGS.Length -ne 4 )
{
    Write-Host "Usage: $0 MerchantId StartDate EndDate OutputFilename"
    Write-Host "Date format is UTC ISO8601 yyyy-mm-ddThh:mm:ssZ eg. 2010-12-01T12:31:00Z Filename"
    exit 1
}

# fetch configuration from command line arguments
$MERCHANT_ID = $ARGS[0]
$START_DATE = $ARGS[1]
$END_DATE = $ARGS[2]
$OUTPUT_FILENAME = $ARGS[3]

# encode header data for use in URL
Add-Type -AssemblyName System.Web
$SAFE_HEADER= [System.Web.HttpUtility]::UrlEncode($HEADER)

# prompt user for password
$PASSWORD = read-host "Please Enter Your Reporting API Password"

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

$FULL_URL="https://$SERVERHOST/history/version/1/merchant/$MERCHANT_ID/transaction"+"?timeOfRecord.start=$START_DATE&timeOfRecord.end=$END_DATE&columns=$COLUMN&columnHeaders=$SAFE_HEADER"
# configure the http client
try
{
    $webclient = New-Object MyWebClient
    $webclient.Credentials = New-Object System.Net.NetworkCredential -ArgumentList "merchant.default", $PASSWORD
    $webclient.DownloadFile($FULL_URL,$OUTPUT_FILENAME)
}
catch [System.Net.WebException]
{
    Write-Host $_.Exception
	# get the actual the text recieved from server
    Write-Host  (New-Object System.IO.StreamReader($_.Exception.Response.GetResponseStream(), [System.Text.Encoding]::GetEncoding("utf-8"))).ReadToEnd()
    exit 2
}