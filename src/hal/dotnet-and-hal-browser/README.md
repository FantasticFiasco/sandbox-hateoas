# .NET Core backend with the HAL-browser as client

## Running the code

Run the following commands in a Bash or PowerShell prompt.

```bash
docker build -t hal .
docker run -p 5000:80 hal
```

When the Docker container is running hit [http://localhost:5000/#/api](http://localhost:5000/#/api) with your favorite web browser.

You will be presented with the following view.

![HAL-browser](hal-browser.png)