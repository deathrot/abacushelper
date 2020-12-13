echo 'Removing existing container'
docker rm abacus

echo 'Running mysql in docker'
docker run --name=abacus -p 3306:3306 -e MYSQL_DATABASE=abacus -e MYSQL_USER=abacus -e MYSQL_PASSWORD=abacus2020 -e MYSQL_ROOT_PASSWORD=abacus2020 -d mysql:latest
