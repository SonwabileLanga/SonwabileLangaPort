# Sonwabile Langa Portfolio

A responsive portfolio website built with ASP.NET Core Razor Pages on .NET 10.

## Run locally

```bash
dotnet run
```

Open the HTTP or HTTPS address printed in the terminal.

## Build

```bash
dotnet build
```

## Deploy to Render

Create a Web Service from this repository and choose **Docker** as the runtime.
Leave the root directory blank and use `./Dockerfile` as the Dockerfile path.
The container listens on port `10000`.

Add these environment variables in Render:

- `RESEND_API_KEY`: your Resend API key. Keep it in Render, never in source control.
- `RESEND_FROM_EMAIL`: a sender address on a domain verified with Resend, for example `Sonwabile Langa <contact@your-verified-domain.com>`.

The contact form delivers messages to `langasonwabile1993@gmail.com`; visitors' addresses are set as the reply-to address. The Resend sender domain must be verified before sending.

If an API key has been shared in chat or committed accidentally, revoke it in Resend and create a replacement before deploying.
