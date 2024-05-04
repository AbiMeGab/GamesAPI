### VIDEO GAMES API

Gadi Medero

![alt text](image-2.png)

Preview of Swagger, how the endpoints looks like.

### Proofs of every endpoint with POSTMAN.

### (C) POST

Create a new game in the Repository.

![alt text](image-1.png)

Possible errors:

Existing ID.

![alt text](image-3.png)

Existing Name.

![alt text](image-6.png)

Wrong Platform.

![alt text](image-4.png)

Wrong Price.

![alt text](image-5.png)

### (R) GET

Retrieval all video games from the repository.

![alt text](image-8.png)

Retrieval a single video game by its ID from the repository.

![alt text](image-7.png)

Error by not found ID.

![alt text](image-9.png)

### (U) PUT

Apdate an existing video game to the repository.

![alt text](image-10.png)

Non existing video game error:

![alt text](image.png)

### (D) DELETE

Delete an existing video game from the repository.

![alt text](image-11.png)

### LAST UPDATES!

Swagger preview.

![alt text](image-12.png)

### New User and Admin accounts feature

![alt text](image-13.png)

### New restrictions

No duplicates are allowed, authentication restrictions and strong password rules.

![alt text](image-14.png)

![alt text](image-15.png)

![alt text](image-16.png)

Only admin users with their Token can generate other admin accounts and post, update and delete games.

![alt text](image-17.png)

![alt text](image-19.png)

![alt text](image-18.png)

![alt text](image-25.png)

![alt text](image-24.png)

Everybody can register a normal account.

![alt text](image-20.png)

If your account does not exist or your credentials are wrong, we will notify you of the error.

![alt text](image-21.png)

This API was tested on Postman (for learning purposes) and with NUnit6.

![alt text](image-22.png)

![alt text](image-23.png)

## Last Updates

Code 201 Created, for the creation of a new game through the method GetById, that can be used at the same time to confirm the existence of the game that we add to the storage with the endpoint POST.

![alt text](image-26.png)

* Testing

![alt text](image-27.png)

1. Create two new games and return Code 201 Created.

2. Verify that the games were created successfully and return Code 200 OK.

3. Verify that the names of the games that were created coincide with the names of the games that were returned in the last check. 

Thank you!