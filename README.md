# MS3SampleApi
Sample API project to share with MS3<br>
<br>
There are three parts to this project.<br>

1.  Database:<br>
   a.  There are three table scripts which need to be applied to a fresh database.<br>
   b.  The scripts have PKID and FKID fields, but are not connected by an actual Foreign Key constraint.<br>
   c.  Other than the PKID value, there is no other constraints to prevent duplicate entried at the database level.<br>
   d.  There are also no change triggers or audit tables to worry about for this project, all things you would want in practicle use.<br>
   <br>
     
2.  API Project:<br>
   a.  The API_Sample2 project/Solution is the functioning API that can be built and run.<br>
   b.  <b>**NOTE**</b>:  There is a hard coded connection string constant in the DAL class.  This is where you would set your server and user information for the database access.  <br>
      1.  Yes, I know there is a perfectly good Web.config file where this setting belongs.  Tell that to the first sample which kept supplying its own SQLExpress connection instead of the named connection in that file.  It wasnt' worth the time fighting it and finding out why.<br>
   c.  API_Sample2 has all Get, Post, Put, and Delete functionality in place and ready to be used.<br>
   d.  This project is set to use LocalHost:62695 as its port.  If you change that, you will need to update the GUI project.<br>
   e.  You will need to run this project via the IDE (debug mode is fine) before launching the GUI in a separate instance of Visual Studio.<br>
   <br>
3.  GUI Project:<br>
      a.  MS3_GUI is part of a larger solution that contains MS3_API_Sample (first version that I chose not to use) and API_Sample_test, which is the test project for the DAL layer that was used in the finished API. <br>
        1.  You can use the Create Sample tests to add new entries to the Database if you choose to, or you can manually insert them.<br>
      b.  The GUI project provides some basic access to the API.  It is not complete and it is not pretty.<br>
      c.  Before launching the GUI, you will want to manually seed your database with some sample data.  See the Test Cases to add Fred and Barney.
      d.  Launch the GUI project (it is set to be the primary project in the solution) and it will allow you to do the following: <br>
        1.  Initial screen will display a list of all records in the client data table.<br>
        2.  You will have the ability to see Details, Edit or Delete a record from the Home screen.<br>
        3.  Details will open a screen where you will see not only the data from the initial list, but all the addresses and contact entries for the selected record.
        4.  Edit will allow you to change the data in the client level portion of the record.  You can't edit or change the addresses or contact information at this time.<br>
        5.  Delete will remove the client record set, including addresses and contacts
        6.  There is no ability to create a new record or add sub records such as Address or Contact at this time.
    
