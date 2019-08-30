## Task

Create an ASP.Net MVC (.Net Framework (Not .Net Core)) website with a page containing a text box to enter a name in and a submit button to search GitHub for the name.

Have the back end call the GitHub users API (e.g. https://api.github.com/users/robconery) and get the users name, location and avatar url from the returned json. Use the repos_url value to get a list of all the repos for the user. Do not use Octokit or any other third party tool for managing api connection.

On the results page, show the username, location, avatar and the 5 repos with the highest stargazer_count.
