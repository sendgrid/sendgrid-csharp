# sendgrid-events-webhook-consumer
This is dockerized SendGrid Event webhook consumer.
# Overview

SendGrid has an [Event Webhook](https://sendgrid.com/docs/API_Reference/Event_Webhook/event.html) which posts events related to your email activity to a URL of your choice. This is an easily deployable solution that allows for customers to easiy get up and running processing (parse and save) their event webhooks. 

This is docker-based solution which can be deployed on cloud services like Heroku out of the box.

# Table of Content
- [Prerequisite](#prerequisite)
- [Deploy locally](#deploy_locally)
- [Deploy Heroku](#deploy_heroku)
- [Testing the Source Code](#testing_the_source_code)

<a name="prerequisite"></a>
## Prerequisite

Clone the repository
```
git clone https://github.com/KoditkarVedant/sendgrid-event-webhook-consumer.git
```

Move into the clonned repository
```
cd sendgrid-event-webhook-consumer
```

Restore the Packages
```
dotnet restore
```

<a name="deploy_locally"></a>
## Deploy locally
Setup your MX records. Depending on your domain name host, you may need to wait up to 48 hours for the settings to propagate.

Run the Event Webhook Parse listener in your terminal:
```
git clone https://github.com/KoditkarVedant/sendgrid-event-webhook-consumer.git

cd sendgrid-event-webhook-consumer

dotnet restore

dotnet run --project .\Src\EventWebhook\EventWebhook.csproj
```
Above will start server listening on a random port like below

In another terminal, use ngrok to allow external access to your machine:
```
ngrok http PORT_NUMBER
```

You can use setup the Event Webhook please refer [this](https://sendgrid.com/docs/API_Reference/Event_Webhook/getting_started_event_webhook.html#-Getting-started)

<a name="deploy_heroku"></a>
## Deploy to Heroku

[Create](https://signup.heroku.com/) Heruko account if not already present

Install the Heroku CLI

Download and install the [Heroku CLI](https://devcenter.heroku.com/articles/heroku-command-line).

If you haven't already, log in to your Heroku account and follow the prompts to create a new SSH public key.
```
$ heroku login
```

Now you can sign into Container Registry.
```
$ heroku container:login
```

Create app in heroku
```
$ heroku apps:create UNIQUE_APP_NAME
```

Create docker image
```
$ docker image -t DOCKER_IMAGE_NAME .
```

Push your Docker-based app
Build the Dockerfile in the current directory and push the Docker image.
```
$ heroku container:push web --app UNIQUE_APP_NAME
```

Deploy the changes
Release the newly pushed images to deploy your app.
```
$ heroku container:release web --app UNIQUE_APP_NAME
```

<a name="testing_the_source_code"></a>
## Testing the Source Code
You can get all the test cases inside the `Tests` folder.
