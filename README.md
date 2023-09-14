# hacker-news-api

Build docker

`docker build -t hackernewsapi ./HackerNewsApi`

Run docker

`docker run -p 8080:80 hackernewsapi`

Result:
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://[::]:80
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Production
info: Microsoft.Hosting.Lifetime[0]
      Content root path: /app
```

Example request: `http://localhost:8080/api/News?count=123` or use swagger: `http://localhost:8080/swagger/index.html`

```
[
  {
    "commentCount": 28,
    "postedBy": "eptcyka",
    "uri": "https://mullvad.net/en/blog/2023/9/13/bug-in-macos-14-sonoma-prevents-our-app-from-working/",
    "score": 984,
    "time": "2023-09-13T17:05:44+00:00",
    "title": "Bug in macOS 14 Sonoma prevents our app from working"
  },
  {
    "commentCount": 39,
    "postedBy": "kcorbitt",
    "uri": "",
    "score": 921,
    "time": "2023-09-12T16:53:51+00:00",
    "title": "Fine-tune your own Llama 2 to replace GPT-3.5/4"
  },
...
 {
    "commentCount": 14,
    "postedBy": "olsgaarddk",
    "uri": "https://www.bitecode.dev/p/the-easy-way-to-concurrency-and-parallelism",
    "score": 85,
    "time": "2023-09-14T06:14:10+00:00",
    "title": "An easy way to concurrency and parallelism with Python stdlib"
  }
]

```

Build & run from bash/cmd

Open solution
cmd: `cd ./HackerNewsApi`, `dotnet run`

Additions/Comments:

I implemented the cache option.
What could be done:
1) Limit the number of requests (semaphor or other 'heap toggle')
2) Make scalable microservices which call db by any message bus (rabbitmq... or mb in case when we have A LOT requests ELK)
3) Db cache
4) trottling of requests for external api.

other:
- manual configuration of the number of threads per microservice / threads in pool (this is about performance).
- optimize models and create custom json converter rule for model
- all recomendation from msdn about asp.net core (webapi). (https redirection, htst and other).
