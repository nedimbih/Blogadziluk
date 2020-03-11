Running the app

	The app is built according to specs you provided so those specs can be used as a guide. 
  There is nothing special that needs to be mentioned, but I will write instructions anyways.
	App has db preloaded with some data so all you need to do is just to start it. Also SSL certificates are turned off.
  


Brief instructions:

  After you load the app in Visual Studio, you can run it simply by hitting F5.

  After starting the app, a browser window will pop up and you need to copy the address from the browser to Postman. 

  App can only be used with Postman or something similar. App does not have UI ( I opted to use a preview version of blazor framework which, as it turns out, has issues. According to github thread that issue is solved, but according to my machine it is still an issue, so I removed the UI that I was planning to build).

You will need to paste the address (that you copied from browser) in postman. 
That address should be http://localhost:52634/ Zou need to add remaining parts as specified in the Task you have sent me. 
Addresses should be 
http://localhost:52634/api/posts/
http://localhost:52634/api/tags 
http://localhost:52634/api/posts/?tag=sometag
etc


