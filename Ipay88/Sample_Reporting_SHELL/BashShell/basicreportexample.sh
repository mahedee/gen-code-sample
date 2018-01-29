#!/bin/bash

# List of columns to output in report.
COLUMN="timeOfRecord,order.id,transaction.id,sourceOfFunds.provided.card.number,sourceOfFunds.provided.card.expiry.month,sourceOfFunds.provided.card.expiry.year,transaction.amount,transaction.currency,transaction.type,transaction.acquirer.id,response.acquirerCode"

# Headings to display for columns. To omit a header row, leave empty.
HEADER="time,order id,transaction id,card number,card expiry month,card expiry year,amount,currency,type,acquirer,acquirerCode"

# Hostname of reporting/history download service
HOST=""

if [ ${#HOST} == 0 ] ; then
   echo "HOST configuration in this script is empty, it must contain reporting/history server hostname"
   exit 1
fi

# Check for correct number of command line parameters
if [ $# -ne 4 ] ; then
   echo "Usage: $0 MerchantId StartDate EndDate OutputFilename"
   echo "Date format is UTC ISO8601 yyyy-mm-ddThh:mm:ssZ eg. 2010-12-01T12:31:00Z Filename"
   exit 1
fi

# Set command line parameters to variables
MERCHANT_ID=$1
START_DATE=$2
END_DATE=$3
OUTPUT_FILENAME=$4

# Convert header list characters to be url safe
SAFE_HEADER=$(echo $HEADER | sed 's/ /%20/g;s/!/%21/g;s/"/%22/g;s/#/%23/g;s/\$/%24/g;s/\&/%26/g;s/'\''/%27/g;s/(/%28/g;s/)/%29/g;s/:/%3A/g')

# Create URL 
FULL_URL="https://$HOST/history/version/1/merchant/$MERCHANT_ID/transaction?timeOfRecord.start=$START_DATE&timeOfRecord.end=$END_DATE&columns=$COLUMN&columnHeaders=$SAFE_HEADER"

echo "Using URL: $FULL_URL"

# Download report
curl --compressed -u "merchant.default" $FULL_URL -o $OUTPUT_FILENAME
