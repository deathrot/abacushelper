# Abacus Helper

Abacus helper is a web app I created to help my kid with his abacus skills like formulas, power excercise, mental maths and time tables. The app backend is designed to keep track of kids process by saving the data in a mysql database.

This repository contains:

1. .Net core web application called App
2. .Net core web application called StudentPortal
3. Business library project
4. Test NUnit test project

## Background

I designed this app to keep my son's abacus skills sharp while in the lockdown. The app got expanded to what it is right now... 

1. English questions
2. Advanced questions type like equation
3. Configurable Quiz(IN PROGRESS)
4. Level promotions(IN PROGRESS)
5. Configural study plans(IN PROGRESS)


## Components

The app is split into the following components:

1. SPA built in react 
1. Backend aspnet core web application 
1. Mysql database

## Install

The app can be containerized using the DockerFile supplied in the App folder, the commands are as follows:

> docker build . -t abacus_helper:latest<br />
> docker run -d -p 80:80 abacus_helper:latest

## Contributions

None right now
