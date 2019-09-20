# Foodify - Discord Hack Week

![alt text](https://cdn-images-1.medium.com/max/2600/1*lh6NS8hx0pu5mlZeSqnu5w.jpeg)

Recipe Foodify is a discord bot that can help you find great recipes for whatever you are looking for.

----
Program utilizes Dependency injection to manage libraries and allow for future testing. Recipe information was scraped from Allrecipes.com and displayed through Discord .Net embeds.

## Configuration

Create a folder called Config inside the BotCore folder. Inside the config folder, add a file called BotToken.json and put your token surrounded in quotes inside it.

## Compiling
**Visual Studio**
- [Visual Studio Code or Visual Studio 2019](https://visualstudio.microsoft.com/downloads/)
- [.Net Core SDK](https://dotnet.microsoft.com/download)

**Command Line**
- [.Net Core SDK](https://dotnet.microsoft.com/download)

## Usage

Call your bot inside the server like this to get a recipe

```
@<BotName> recipe chicken 
```
Call your bot inside the server like this to get multiple results of a search
```
@<BotName> top tuna 
```
## Libraries
- [Discord.Net 2.1.1](https://github.com/discord-net/Discord.Net)

- [Unity 5.11.1](https://github.com/unitycontainer/unity)

- [HtmlAgilityPack 1.11.8](https://html-agility-pack.net/)
