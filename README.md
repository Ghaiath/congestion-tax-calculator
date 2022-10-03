# Congestion Tax Calculator

Run `docker-compose up` up to start the services

This will seed the MongoDB with the initial data.

Not all endpoints implemented. Only get of tax rules and get congestion tax.

Once you run `docker-compose up`, surf to http://localhost:8000/swagger/index.html

```
{
  "vehicleType": "CAR",
  "dates": [
    "2013-01-14 21:00:00",
    "2013-01-15 21:00:00",
    "2013-02-07 06:23:27",
    "2013-02-07 15:27:00",
    "2013-02-08 06:27:00",
    "2013-02-08 06:20:27",
    "2013-02-08 14:35:00",
    "2013-02-08 15:29:00",
    "2013-02-08 15:47:00",
    "2013-02-08 16:01:00",
    "2013-02-08 16:48:00",
    "2013-02-08 17:49:00",
    "2013-02-08 18:29:00",
    "2013-02-08 18:35:00",
    "2013-03-26 14:25:00",
    "2013-03-28 14:07:27"
  ]
}
```
And got the follwing response: 
```
{
  "01/14/2013": "0",
  "01/15/2013": "0",
  "02/07/2013": "21",
  "02/08/2013": "39",
  "03/26/2013": "8",
  "03/28/2013": "8"
}
```