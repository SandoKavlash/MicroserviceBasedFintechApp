
ჩამოკლოვნის შემდეგ Solution-ის ფოლდერში უნდა დადგეთ და გაუშვათ ეს ბრძანებები
docker build --pull -t paymentservice --file MicroserviceBasedFintechApp.PaymentService.Api/Dockerfile .
docker build --pull -t orderservice --file MicroserviceBasedFintechApp.OrderService.Api/Dockerfile .
docker build --pull -t identityservice --file MicroserviceBasedFintechApp.Identity.Api/Dockerfile .

ეს იმისათვის არის საჭირო რომ შეიქმანს 3 image paymentservice, orderservice da identityservice

ამის შემდეგ ერთი ცალკე ნეთვორქი უნდა შექმნათ, რადგან bridge network არ არიზოლვებს კონტეინერის სახელებით Ip-ებს.
ანუ შემდეგ ამ ბრძანებას უშვებთ ახალი network-ის შესაქმნელად: docker network create test


ამის შემდეგ ამ ბრძანებებს უშვებთ სათითაოდ, რათა ყველა საჭირო კონტეინერი დაისტარტოს.(ყველა კონტეინერს test ნეთვორქი აქვს მითითებული)
docker run --name my_postgres_db -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=fintechapp -e POSTGRES_DB=fintech_app_db -p 1234:5432 -d --network=test postgres
docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 --network=test rabbitmq:management
docker run -d -p 7001:8080 -e "ASPNETCORE_ENVIRONMENT=Development" --network=test --name orderservice orderservice
docker run -d -p 7002:8080 -e "ASPNETCORE_ENVIRONMENT=Development" --network=test --name paymentservice paymentservice
docker run -d -p 7003:8080 -e "ASPNETCORE_ENVIRONMENT=Development" --network=test --name identityservice identityservice

