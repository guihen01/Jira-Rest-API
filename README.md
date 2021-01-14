# Jira-Rest-API
coded in C#
use Json package

# how to use
allow to make a rest api request to jira server 
it is based on rest api library of Atlassian 
ref : https://docs.atlassian.com/software/jira/docs/api/REST/8.13.2/
the excutable code when lounched ask some questions for starting the corresponding rest API

# parameters 
1) pathanme and name of the json file   
   could be :  C:\C#Rest-API\Execute-RestAPI-POST\Execute-Jira-RestAPi\create-issue.json
2) URL of the jira server : that is the point on which to send the REst API httprequest :  
  ( ie : exemple : http://localhost:8080/rest/api/2/issue )
3) auth : username and password of a granted account to jira server

# it is distributed as nuget package 
reference to download the package is : 
https://www.nuget.org/packages/Execute-Jira-RestAPi/
