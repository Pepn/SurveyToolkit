# SurveyToolkit


## Uploading Data
The default method uses an unprotected WWW file upload to upload the recorded file to a server capeable of running PHP. Follow these steps to setup a free server space (2023)
- Create an acccount and hosting on [000webhost](http://000webhost.com/).
- Upload the getFile.php and the empty /data folder to your hosting.
- Copy the link to the getFile.php from the server, should be something like: [https://xxxxxx.000webhostapp.com/getFile.php](https://xxxxxx.000webhostapp.com/getFile.php).
- In Unity select your SurveyManager object and paste it into the uploadLink variable & enable the uploadToServer checkbox.

Remarks: This is not a very safe solution. While the getFile.php script has some tamper proofing it is not considered safe to allow unverified persons or scripts to upload on your server. Therefore, we recommend you to only host this script when actually collecting data.
