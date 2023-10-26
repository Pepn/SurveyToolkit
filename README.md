# SurveyToolkit
The QuestionnaireToolkit is a software tool designed for the creation of questionnaires, surveys, and straightforward user interface forms for applications across VR, Desktop, and Mobile.

This tool offers a plug and play non-scripting solution to create beautiful questionnaires & surveys. Furthermore, it includes a straightforward instructional manual detailing the setup procedure for a basic backend system, which stands prepared for data collection online, if deemed necessary. Consequently, developers can allocate their efforts toward addressing other critical, application-specific functionalities.

This toolkit uses default Unity UI and therefore supports VR.

The SurveyToolkit supports the following question types: Open, Linear, Grid, Slider, Checkbox and Multiple Choice.

The questions are created by instantiating ScriptableObjects in the editor. Easy formatting is possible by editing the prefabs which correspond to the question type. 

## Documentation
The SurveyManager contains QuestionnairePages which handle most of the logic for each page. The pages contain a list of FormObjectData (ScriptableObjects) where a FormObjectData could either a question, a submit button or some infobox. Every question is a prefab variant of the original QuestionForm, if you want to change the layout change this prefab. The FormObjectData must have a reference to the prefab, this way the QuestionnairePage knows which and how to instantiate the prefab. The Survey is populated at runtime, so press play to inspect your survey.
To create new questions right click in the Project > Create > SurveyToolkit > QuestionType. Or just copy and edit the example questions.

## Example
The SampleScene shows a simple 2 page survey

## Data
The data is stored as .csv files in the [persistant storage](https://docs.unity3d.com/ScriptReference/Application-persistentDataPath.html) of the device. It is also possible to upload the data to a server.

## Uploading Data
SurveyToolkit uses an C# WWW form to upload the recorded file to a server capeable of running PHP. Follow these simple steps to get a backend running. (2023)
- Create an acccount and hosting on [000webhost](http://000webhost.com/).
- Upload the getFile.php and the empty /data folder to your hosting.
- Copy the link to the getFile.php from the server, should be something like: [https://xxxxxx.000webhostapp.com/getFile.php](https://xxxxxx.000webhostapp.com/getFile.php).
- In Unity select your SurveyManager object and paste it into the uploadLink variable & enable the uploadToServer checkbox.

Remarks: This is not a very safe solution. While the getFile.php script has some tamper proofing it is not considered safe to allow unverified persons or scripts to upload on your server. Therefore, we recommend you to only host this script when actually collecting data.
