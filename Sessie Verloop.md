# Voorstellen

- Wie zijn we?

# Agenda

de codereview als insteek nemen en meteen doorgaan naar de intro

# Intro

insteek van de codereview gebruiken als intro voor peiling

- Werken met wordcloud
- ingaan op de vraag

# SonarQube

Wat is sonarqube?

- Automatische code review tool
  - veilig
  - betrouwbaar
  - onderhoudbaar
- Zowel lokaal als Integreert mooi in heel de devops cyclus
- Onderdeel van Sonar
  - Sonar lint voor in de ide
  - Sonar qube self-hosted solution
    - community versie
  - Sonar cloud
- Sonarqube server + sonarscanner

## Demo

project overlopen (Default Microsoft ContosoUnifersity project)
snippets laten zien van zaken die er door sonarqube worden uitgehaald en vragen wat er mee mis is?

Een verse docker opzetten:

```bash
docker run -d --name sonarqube -p 9000:9000 -p 9092:9092 sonarqube
```

user token generen voor de sonarscanner

sonarscanner gebruiken binnen project

```bash
#dotnet tool install --global dotnet-sonarscanner


dotnet sonarscanner begin /k:"sonardemo" /d:sonar.token="squ_e5a44f23f354d494aae13d80c7ec86304e5e066c"
dotnet build
dotnet sonarscanner end /d:sonar.token="squ_e5a44f23f354d494aae13d80c7ec86304e5e066c"
```

de client omgeving overlopen:

- rules: de regels waar de code tegen getest kan worden
- quality profiles: Tegen welke regels wil ik mijn project testen?
- quality gates: Welke score moet mijn project kunnen behalen binnen het quality profile
- projecten
  - overview: dashboard
  - issues: concrete problemen
  - security hotspots: security sensitive deel code dat gereviewed moet worden
- issues

Makkelijk te integreren met pipeline

sonarqube tool om codekwaliteit te kunnen garanderen rond best practices en security
naar leesbaarheid en uniformiteit zou het

# EditorConfig

binnen een project consistente code style onderhouden

- is zijn eigen ding, niet van MicroSoft, breed toepasbaar
- krijgt in visual studio voorrang op de text editor settings
- hierarchisch in opbouw, mogelijk in solution en dan verder naar beneden
- `.editorconfig`
- In visual studio
  - new file
  - avoid unused parameters

```xml
<PropertyGroup>
  <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
</PropertyGroup>
```

90% van de opmerkingen uit de reviews zijn daarmee opgelost

//Enforce code style in build

....

# Afronding

- Reclame maken voor de summerjob
