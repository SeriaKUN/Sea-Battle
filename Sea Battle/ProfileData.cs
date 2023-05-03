﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace Sea_Battle
{
    public class ProfileData
    {
        public string nickname;
        public int winMatches;
        public int winRounds;
        public int loseMatches;
        public int loseRounds;

        public ProfileData()
        {
            nickname = "unknown";
            winRounds = 0;
            winMatches = 0;
            loseRounds = 0;
            loseMatches = 0;
        }

        public ProfileData(string nickname, int winRounds = 0, int winMatches = 0, int loseRounds = 0, int loseMatches = 0)
        {
            this.nickname = nickname;
            this.winRounds = winRounds;
            this.winMatches = winMatches;
            this.loseRounds = loseRounds;
            this.loseMatches = loseMatches;
        }

        public static void SaveProfileToFile(ProfileData profile)
        {
            FileStream fileStream = new FileStream(profile.nickname + ".xml", FileMode.OpenOrCreate);

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProfileData));
            xmlSerializer.Serialize(fileStream, profile);

            fileStream.Close();
        }

        public static ProfileData LoadProfileFromFile(string profileFilePath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProfileData));
            FileStream fileStream = new FileStream(profileFilePath, FileMode.OpenOrCreate);

            try
            {
                ProfileData profile = (ProfileData)xmlSerializer.Deserialize(fileStream);
                fileStream.Close();
                return profile;
            }
            catch
            {
                fileStream.Close();
                return new ProfileData();
            }
        }

        public override string ToString()
            => $"nickname = {nickname}\nwinRounds = {winRounds}\nwinMatches = {winMatches}\nloseRounds = {loseRounds}\nloseMatches = {loseMatches}";

        public string Description
            => $"{nickname}\nyou won {winRounds} rounds and {winMatches} matches\nyou lost {loseRounds} rounds and {loseMatches} matches";

        public void WinMatch()
            => winMatches++;
        public void WinRound()
            => winRounds++;
        public void LoseMatch()
            => loseMatches++;
        public void LoseRound()
            => loseRounds++;
    }
}
