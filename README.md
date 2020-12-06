# Abacus Helper

Abacus helper is a web app I created to help my kid with his abacus skills like formulas, power excercise, mental maths and time tables. The app backend is designed to keep track of kids process by saving the data in a mysql database.

This repository contains:

1. .Net core web application called App
2. Business library project
3. Test NUnit test project


## Background

This app is designed to keep my kids abacus skills sharp, track his progress and generate reports for the parent to take action on.

## Components

The app is split into the following components:

1. SPA built in react 
1. Backend aspnet core web application 
1. Mysql database

## Install

The app can be containerized using the DockerFile supplied in the App folder, the commands are as follows:

> docker build . -t abacus_helper:latest
> docker run -d -p 80:80 abacus_helper:latest

## Contributions

None right now