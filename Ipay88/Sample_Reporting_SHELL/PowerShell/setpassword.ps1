read-host "Please Enter Your Reporting API Password" -assecurestring | ConvertFrom-SecureString | set-content $HOME\reportingPassword
