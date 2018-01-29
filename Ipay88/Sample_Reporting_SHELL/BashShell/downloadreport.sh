#!/bin/bash

MERCHANT_ID=""
COLUMN="timeOfRecord,order.id,transaction.id,sourceOfFunds.provided.card.number,sourceOfFunds.provided.card.expiry.month,sourceOfFunds.provided.card.expiry.year,transaction.amount,transaction.currency,transaction.type,transaction.acquirer.id,response.acquirerCode"
HEADER="time,order id,transaction id,card number,card expiry month,card expiry year,amount,currency,type,acquirer,acquirerCode"

# hostname of reporting/history download service, see the FULL_URL definition at bottom of script to see how it is used.
HOST=""

# initial date to be used, after first run, the last end date is stored and used instead
START_DATE="2012-01-01T00:00:00Z"
# set end date to now
END_DATE=`date -u "+%FT%TZ" `

# report output filename
OUTPUT_FILENAME="transactions.csv"

# file used to store last end date
LAST_RUN_FILENAME="$HOME/reportingLastRunTime"

# file of netrc file
NET_RC_FILENAME="$HOME/.netrc"

# Don't change, used to check permissions of .netrc file
NET_RC_PERMS=$(stat -c %a $NET_RC_FILENAME)

# text to check for in .netrc file, assumes no username is specified
NET_RC_TEXT="machine $HOST login merchant.default password "

# check that host variable is set
if [ ${#HOST} == 0 ]; then
   echo "HOST configuration in this script is empty, it must contain the reporting/history server hostname"
   exit 1
fi

if [ ${#MERCHANT_ID} == 0 ]; then
   echo "MERCHANT_ID configuration in this script is empty, it must contain your merchant id"
   exit 1
fi

# ensure .netrc file has correct permissions
if [ $(stat -c %u $NET_RC_FILENAME) != ${UID} ] || [ ${NET_RC_PERMS:0:3} != 400 ] ; then
   echo "Expected user file $NET_RC_FILENAME to have user read only permission"
   exit 1
fi

# check .netrc file has hosts entry
if [ $(grep "$NET_RC_TEXT" $NET_RC_FILENAME | wc -l) != "1" ] ; then
   echo "Expected to find \"$NET_RC_TEXT\" text in $NET_RC_FILENAME file"
   exit 1
fi

# update start date from last run's end date
if [ -e $LAST_RUN_FILENAME ]; then
   START_DATE=`cat $LAST_RUN_FILENAME`
fi

# convert header list characters to be url safe
SAFE_HEADER=$(echo $HEADER | sed 's/ /%20/g;s/!/%21/g;s/"/%22/g;s/#/%23/g;s/\$/%24/g;s/\&/%26/g;s/'\''/%27/g;s/(/%28/g;s/)/%29/g;s/:/%3A/g')

function downloadReport {
  # delete previous outputfile
  rm -f $OUTPUT_FILENAME

  # download report
  FULL_URL="https://$HOST/history/version/1/merchant/$MERCHANT_ID/transaction?timeOfRecord.start=$START_DATE&timeOfRecord.end=$END_DATE&columns=$COLUMN&columnHeaders=$SAFE_HEADER"
  HTTP_RESULT=$(curl -sS --compressed -n $FULL_URL -o $OUTPUT_FILENAME -w "%{http_code}")
}

downloadReport

# check if there is an output file
if [ ! -e $OUTPUT_FILENAME ]; then
   echo "output file $OUTPUT_FILENAME does not exist" 1>&2
   exit $HTTP_RESULT
fi


# check for end date error where reporting/history server is lagging behind
if [ $HTTP_RESULT != 200 ]; then
   if [ $(grep "DATA_AVAILABLE_AFTER " $OUTPUT_FILENAME | wc -l) -eq "1" ]; then
     # use end date from server reponse and try to download again
     END_DATE=$(cut -c 22-41 $OUTPUT_FILENAME)
     downloadReport
   fi
fi

# store end date to be used as the start date for next run
if [ $HTTP_RESULT == 200 ]; then
   echo "$END_DATE" > "${LAST_RUN_FILENAME}"
else
   echo "Last run file name not updated due to previous errors during curl download" 1>&2
   #show error on stderr
   cat $OUTPUT_FILENAME 1>&2
   exit $HTTP_RESULT
fi
