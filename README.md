# Microservice JWT Authentication
Microservice Json Web Token Authentication, use: 
* .NET Core 3.1 
* WebApi
* JWT Authentication API
* Entity Framework
* AutoMapper

## Database schema
![Image description](LocalDB/diagram.png)

## Requests
* Get new token
* Check if existing token is valid
* Create a new user

## Request examples:

1. Create a new token:

Method Get:
```console
http://localhost:30796/api/token/get?username=test1&password=123456

Response:
{
    "token_type": "Bearer",
    "access_token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOjEsIm5iZiI6MTU4OTQ0MDYxNCwiZXhwIjoxNTg5NDc2NTE1LCJpYXQiOjE1ODk0NDA1MTUsImlzcyI6IllCQl9Kc29uV2ViVG9rZW5TZXJ2ZXIiLCJhdWQiOiJodHRwczovL3Rlc3R3aXpkaWFwaS5henVyZXdlYnNpdGVzLm5ldCJ9.YWYLiPCdZD936w6Ny4oRPMewswAYcUnhUUGve0YuRTQ"
}
```

2. Check if token valid:

Method Get:
```console
http://localhost:30796/api/token/is-valid?Token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOjEsIm5iZiI6MTU4OTQ0MDYxNCwiZXhwIjoxNTg5NDc2NTE1LCJpYXQiOjE1ODk0NDA1MTUsImlzcyI6IllCQl9Kc29uV2ViVG9rZW5TZXJ2ZXIiLCJhdWQiOiJodHRwczovL3Rlc3R3aXpkaWFwaS5henVyZXdlYnNpdGVzLm5ldCJ9.YWYLiPCdZD936w6Ny4oRPMewswAYcUnhUUGve0YuRTQ

Response:
{
    "expirationDate": "14/05/2020 17:15:15"
}
```

3. Create a new user:

Method Put:
```console
http://localhost:30796/api/user/create

Body Request:
{
"ApplicationId":1,
"FirstName": "test1",
"MiddleName": "one",
"LastName": "test last",
"PicturePath": "pic",
"Email": "email",
"Username": "test2",
"Password": "123456",
"Mobile": "088888",
"Phone": "054111111",
"Comments": "com",
"UserFields": [
	{"FieldTypeName":"childs", "FieldData":"3"},
	{"FieldTypeName":"WifeName", "FieldData":"Elina"}
],
"UserRoles": [
	{"UserRoleId":1}, 
	{"UserRoleId":2}
	]
}

Response:
{
    "id": 18,
    "applicationId": 1,
    "firstName": "test1",
    "middleName": "one",
    "lastName": "test last",
    "picturePath": "pic",
    "email": "email",
    "username": "test2",
    "password": "123456",
    "mobile": "088888",
    "phone": "054111111",
    "birthdate": null,
    "isApproved": false,
    "isLocked": false,
    "isActive": true,
    "lastPasswordChangedDate": "2020-05-14T14:04:22.5468177Z",
    "failedPasswordAttemptCount": 0,
    "comments": "com",
    "createDate": "2020-05-14T14:04:22.5469074Z",
    "application": null,
    "token": [],
    "userField": [
        {
            "id": 1,
            "userId": 18,
            "fieldTypeName": "childs",
            "fieldData": "3",
            "createDate": "2020-05-14T17:04:23.9"
        },
        {
            "id": 2,
            "userId": 18,
            "fieldTypeName": "WifeName",
            "fieldData": "Elina",
            "createDate": "2020-05-14T17:04:23.9"
        }
    ],
    "userUserRole": [
        {
            "id": 20,
            "userId": 18,
            "userRoleId": 1,
            "createDate": "2020-05-14T17:04:23.903",
            "userRole": null
        },
        {
            "id": 21,
            "userId": 18,
            "userRoleId": 2,
            "createDate": "2020-05-14T17:04:23.903",
            "userRole": null
        }
    ]
}
```

## Better implementation 
As we know, each mecroservice must implement one bussiness task. That why in better way we can split that microservice:
* Token service
* User service


## Resources
For documentation and instructions check out http://jasonwatmore.com/post/2018/08/14/aspnet-core-21-jwt-authentication-tutorial-with-example-api
