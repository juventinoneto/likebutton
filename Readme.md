# LikeButtonChallenge

This project provides an approach to solving an action of liking a post on a social network. 

## Running
The project was developed using DotNet 5 and docker-compose to create the containers. For the execution to take place successfully, this requirement must be met.

Enter in the root folder of the project and execute the command:

```bash
docker-compose up -d // Windows
sudo docker-compose up -d // Linux
```
That command will generate three containers: sql1, redis and rabbitmq_management. You can check if the containers are up with the follow command (after the download of the images):
```bash
docker container ps // Windows
sudo docker container ps // Linux
```
In case that you don't have dotnet ef cli installer, you can install it with the command below:
```bash
dotnet tool install --global dotnet-ef
```

After running docker-compose, it will also be necessary to create the tables from the migrations already defined. Run the following commands:

```bash
cd Infrastructure/
dotnet ef --startup-project ../API/API.csproj database update
```
Open the project in your IDE and execute the project.
  
## Usage
The project has 4 engpoints: create an article, get all articles, get an article and like an article. They can be called by the tool of you preference.

## Architecture

![Untitled Diagram](https://user-images.githubusercontent.com/3699136/134031504-b48f972a-8128-466a-b5f3-d98e9f575f26.jpg)
