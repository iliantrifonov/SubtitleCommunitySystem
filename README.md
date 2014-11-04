SubtitleCommunitySystem
=======================

An ASP.NET MVC application, for adding, searching and managing subtitles. There are teams, and each team has users with roles that are part of the subtitle creating process, with a workflow for them.

* Display of the latest subtitles on the main page, with links to details for each one.
* Search by name of the subtitles.
* Languages.
* Workflow for teams:
* Roles in each team: Translator, Sync, ImageManager, Revisioner (approves the final subtitle, and writes a summary)
* Site roles: Standard (each user, can apply to be a part of a team for certain language/series/movie ), Moderator (has the ability to edit the content of each subtitle post, edit news etc.), Admin (can promote users to the various roles, has access to the chats of all teams).
* Roles for news: Writer
* Chat: Each team has a chat channel, where they can discuss the various parts of the project.
* Each subtitle has stages that it passes on the workflow, so it can be seen on what stage of development each subtitle is.
* Calendar for scheduled releases.
* Section for news on upcoming movies/shows/subtitles.
* Top subtitles by downloads for the week.
* Each user that is part of a team can view the progress of subtitles it produces.
