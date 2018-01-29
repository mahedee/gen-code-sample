#!/bin/sh
#
# Hosted Batch interface
#
# Upload files to the Hosted Batch system.
# Program exits with: 0 when run successfully, non-0 when an error has occurred.
#
# Requirements: 
#    1. curl, openssl, and lsof "list open files"
#    2. input, output, and pending directories
#
# Operation:
#   1. Modify the configuration parameters below to suit your integration.
#   2. Create your .netrc file.
#   3. Create a cron job to execute the hb.sh script on a recurring basis (i.e., every 5 minutes).
#   4. Place the batch file into the "input" directory.
#   5. When the script executes it will:
#        - Upload any batch files in the "input" directory to Hosted Batch and then move them to the "pending" directory.
#        - Verify the batch upload by sending a Message Integrity Code.
#        - Upon completion of processing, place into the "output" directory the original request and the downloaded response.
#

# Configuration: Hosted Batch application
VERSION_NUM=9
HB_HOST="<HB_HOST>"

# Configuration: Merchant
MERCHANT_ID="<YOUR_MERCHANT_ID>"

# Configuration: Advanced
CONNECT_URL="https://$HB_HOST/batch/version/$VERSION_NUM/merchant/$MERCHANT_ID/batch/"
HTTP_HEADERS="Content-Type: text/plain; charset=UTF-8"


# Configuration: This application
INPUT_DIR=./input
OUTPUT_DIR=./output
PENDING_DIR=./pending
CURL_EXEC="curl"

# file of netrc file
NET_RC_FILENAME="$HOME/.netrc"

# Don't change, used to check permissions of .netrc file
NET_RC_PERMS=$(stat -c %a $NET_RC_FILENAME)

# text to check for in .netrc file, assumes no username or login is specified
NET_RC_TEXT="machine $HB_HOST password"

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


# Create directories if they don't already exist
if [ ! -d $INPUT_DIR ]; then
	echo "Creating missing $INPUT_DIR"
	mkdir -p $INPUT_DIR
fi
if [ ! -d $PENDING_DIR ]; then
	echo "Creating missing $PENDING_DIR"
	mkdir -p $PENDING_DIR
fi
if [ ! -d $OUTPUT_DIR ]; then
	echo "Creating missing $OUTPUT_DIR"
	mkdir -p $OUTPUT_DIR
fi

# Upload batch files to Hosted Batch application
#

if [ "$(ls -A $INPUT_DIR/)" ]; then
	for f in $INPUT_DIR/*
	do

		# Check that the batch file is not already open and being used.
		lsof "$f" | grep -q COMMAND &>/dev/null
		if [ $? -ne 0 ]; then

			# Get the batch name
			BATCH_NAME=`basename $f`

			# Upload to Hosted Batch
			RESP=`$CURL_EXEC -n -s -w "%{http_code}" -T $f -H "$HTTP_HEADERS" "$CONNECT_URL$BATCH_NAME" -o hbLastResponse.txt`
			LAST_ERROR="$?"

			if [ "$LAST_ERROR" != "0" ]; then
				# Errors executing cURL
				echo "ERR: cURL application error $LAST_ERROR: $BATCH_NAME"
				exit -1

			elif [ "$RESP" != "200" ]; then
				# Unexpected HTTP(s) response
				echo "ERR: HTTP(s) response $RESP: $BATCH_NAME"
				cat hbLastResponse.txt
				echo
				exit $RESP

			else
				# File upload successful, move to working folder
				mv $INPUT_DIR/$BATCH_NAME $PENDING_DIR/$BATCH_NAME
				echo "OK: Uploaded: $BATCH_NAME"

			fi

		else
			echo "ERR: Batch file is already open: $f"

		fi

	done
fi


# Check pending batch files
#

if [ "$(ls -A $PENDING_DIR/)" ]; then
	for f in $PENDING_DIR/*
	do

		# Get the batch name
		BATCH_NAME=`basename $f`

		# Check the status of the batch in Hosted Batch
		RESP=`$CURL_EXEC -n -s -H "$HTTP_HEADERS" "$CONNECT_URL$BATCH_NAME/status"`
		LAST_ERROR="$?"

		if [ "$LAST_ERROR" != "0" ]; then
			# Errors executing cURL
			echo "ERR: cURL application error $LAST_ERROR: $BATCH_NAME"
			exit -1

		elif [[ ! "$RESP" =~ \"$BATCH_NAME\" ]]; then
			# Batch name not found in hosted batch response
			echo "ERR: Batch not found in response: $BATCH_NAME"
			exit -2

		else
			# Batch found - check its status

			if [[ "$RESP" =~ \"UPLOADED\" ]]; then
				# Batch uploaded, validate using SHA1
				CHECKSUM=`openssl sha1 $f`
				CHECKSUM=${CHECKSUM#SHA1($f)= }
				RESP=`$CURL_EXEC -n -s -H "$HTTP_HEADERS" "$CONNECT_URL$BATCH_NAME/validate" -o /dev/null -d "$CHECKSUM"`
				echo "OK: Validated: $BATCH_NAME"

			elif [[ "$RESP" =~ \"COMPLETE\" ]]; then
				# Batch complete, download results and back up original input file
				RESP=`$CURL_EXEC -n -s -H "$HTTP_HEADERS" "$CONNECT_URL$BATCH_NAME/response" -o $OUTPUT_DIR/$BATCH_NAME`
				mv $PENDING_DIR/$BATCH_NAME $OUTPUT_DIR/$BATCH_NAME.original
				echo "OK: Batch complete: $BATCH_NAME"

			else
				echo "OK: Batch processing: $BATCH_NAME"

			fi

		fi

	done
fi


exit 0
