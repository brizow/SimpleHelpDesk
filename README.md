# SimpleHelpDesk

Details:
This little .net app allows for a user to fill a form with information then submit it.
It relies on a working Active Directory server to function. The top labels of information
will be populated with info once connected correctly.

Rebuilt with bootstrap 3.
Minimalist styling.

How to setup:
web.config:
Fill in the Mail Server details to match your server or relay.
Edit the connectionstring to point to your AD server.
Change the Authentication method to Windows.

IIS:
Push this site up to your local IIS server. 
Verify your IIS Security settings, set them to Windows Authentication.

Check the site after publishing to IIS. Resolve any errors and test submit a "ticket" via
the form.

Things to work on:
Easier setup method. Perhaps a form with assistance for connecting to AD.
Some sort of Admin section. The item above would go there.
Create a ticket portal.
Assign tickets.

--An independent database option with user sign in or sign up.
