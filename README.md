# VS - Lookup Matchups! 

Check matchup data to see which champions had the most favorable lane matchups in URF mode! See highest and lowest win rates overall or for a specific lane matchup, or see which champions were most popular.

This program uses the Match endpoint's Participants.Timeline.Lane property to determine which champions are laning against each other, instead of calculating based on whether two champions are on opposite teams.

##Features

- View champions with the highest or lowest win rates as well as champions with the most popularity.
- Check out lists of a champion's favorable/unfavorable matchups: which enemy laners give lower/higher win rates? Which champions should you choose if you hate laning against a certain champion in URF mode?
- See how well others have done in a lane with a specific champion and specific enemy laner.

##Media

- [Screenshots](http://imgur.com/a/4n1L1)
- [Video](http://www.google.com)

##Requirements

- [Microsoft .NET Framework 4.5](http://www.microsoft.com/en-us/download/details.aspx?id=30653)

##Installation

1. Download and extract compiled application [here](https://github.com/aktai0/VS---Lookup-Matchups-/releases) into a folder.

or

1. Get and extract the source.

2. Build the project in Visual Studio.

##Usage

- Create a file named `riot_key` (no extension) with a Riot API key and save it in the same output folder as VS.exe.

- Click "Load Matches" and "Start" to begin loading in match data. Click Slow API if using a developer API key (up to 10 calls / 10 seconds), or Fast API if using a production API key (up to 3000 calls / 10 seconds).

- Click "Stop" to stop gathering data and browse information about matchups.

##Libraries used

- [RiotSharp](https://github.com/BenFradet/RiotSharp)
- [Image ComboBox Control](http://www.codeproject.com/Articles/10670/Image-ComboBox-Control)

##License

See LICENSE file.

##Disclaimer
VS - Lookup Matchups! isn't endorsed by Riot Games and doesn't reflect the views or opinions of Riot Games or anyone officially involved in producing or managing League of Legends. League of Legends and Riot Games are trademarks or registered trademarks of Riot Games, Inc. League of Legends © Riot Games, Inc.