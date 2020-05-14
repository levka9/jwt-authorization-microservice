# microservice-jwt-authorization
Microservice Json Web Token Authorization use: .NET Core 3.1 - JWT Authentication API

## Request examples:

1. Create a new token:
Method Get:
http://localhost:30796/api/token/get?username=test1&password=123456

Response:
{
    "token_type": "Bearer",
    "access_token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOjEsIm5iZiI6MTU4OTQ0MDYxNCwiZXhwIjoxNTg5NDc2NTE1LCJpYXQiOjE1ODk0NDA1MTUsImlzcyI6IllCQl9Kc29uV2ViVG9rZW5TZXJ2ZXIiLCJhdWQiOiJodHRwczovL3Rlc3R3aXpkaWFwaS5henVyZXdlYnNpdGVzLm5ldCJ9.YWYLiPCdZD936w6Ny4oRPMewswAYcUnhUUGve0YuRTQ"
}

2. Check if token valid:

Method Get:
http://localhost:30796/api/token/is-valid?Token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOjEsIm5iZiI6MTU4OTQ0MDYxNCwiZXhwIjoxNTg5NDc2NTE1LCJpYXQiOjE1ODk0NDA1MTUsImlzcyI6IllCQl9Kc29uV2ViVG9rZW5TZXJ2ZXIiLCJhdWQiOiJodHRwczovL3Rlc3R3aXpkaWFwaS5henVyZXdlYnNpdGVzLm5ldCJ9.YWYLiPCdZD936w6Ny4oRPMewswAYcUnhUUGve0YuRTQ

Response:
{
    "expirationDate": "14/05/2020 17:15:15"
}

## Resources
For documentation and instructions check out http://jasonwatmore.com/post/2018/08/14/aspnet-core-21-jwt-authentication-tutorial-with-example-api
