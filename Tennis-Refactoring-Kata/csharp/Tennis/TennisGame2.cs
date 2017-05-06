namespace Tennis
{
    public class TennisGame2 : ITennisGame
    {
        private const string LOVE_SCORE = "Love";
        private const string FIFTEEN_SCORE = "Fifteen";
        private const string THIRTY_SCORE = "Thirty";
        private const string DRAW_SCORE_POSTFIX = "-All";
        private const string DEUCE_SCORE = "Deuce";
        private const int ZERO_POINTS = 0;
        private const int FIFTEEN_POINTS = 1;
        private const int THIRTY_POINTS = 2;
        private const int FORTY_POINTS = 3;
        private const string EMPTY_SCORE = "";
        private const string FORTY_SCORE = "Forty";
        private const string SCORE_SEPARATOR_TOKEN = "-";
        private const int ADVANTAGE_POINTS = 4;
        private const string PLAYER1_ADVANTAGE_SCORE = "Advantage player1";
        private const string PLAYER2_ADVANTAGE_SCORE = "Advantage player2";
        private int player1Points;
        private int player2Points;

        private string player1Result = "";
        private string player2Result = "";
        private string player1Name;
        private string player2Name;

        public TennisGame2(string player1Name, string player2Name)
        {
            this.player1Name = player1Name;
            player1Points = 0;
            this.player2Name = player2Name;
        }

        public string GetScore()
        {
            var score = EMPTY_SCORE;
            bool tiedBelowFourtyPoints = (player1Points == player2Points) && (player1Points < FORTY_POINTS);

            if (tiedBelowFourtyPoints)
            {
                if (player1Points == ZERO_POINTS)
                    score = LOVE_SCORE;
                if (player1Points == FIFTEEN_POINTS)
                    score = FIFTEEN_SCORE;
                if (player1Points == THIRTY_POINTS)
                    score = THIRTY_SCORE;
                score += DRAW_SCORE_POSTFIX;
            }

            bool tiedAboveThirtyPoints = (player1Points == player2Points) && (player1Points > THIRTY_POINTS);
            if (tiedAboveThirtyPoints)
                score = DEUCE_SCORE;

            bool player1HasPoints = player1Points > ZERO_POINTS;
            bool player2HasNoPoints = player2Points == ZERO_POINTS;
            if (player1HasPoints && player2HasNoPoints)
            {
                if (player1Points == FIFTEEN_POINTS)
                    player1Result = FIFTEEN_SCORE;
                if (player1Points == THIRTY_POINTS)
                    player1Result = THIRTY_SCORE;
                if (player1Points == FORTY_POINTS)
                    player1Result = FORTY_SCORE;

                player2Result = LOVE_SCORE;
                score = player1Result + SCORE_SEPARATOR_TOKEN + player2Result;
            }

            bool player2HasPoints = player2Points > 0;
            bool player1HasNoPoints = player1Points == 0;
            if (player2HasPoints && player1HasNoPoints)
            {
                if (player2Points == FIFTEEN_POINTS)
                    player2Result = FIFTEEN_SCORE;
                if (player2Points == THIRTY_POINTS)
                    player2Result = THIRTY_SCORE;
                if (player2Points == FORTY_POINTS)
                    player2Result = FORTY_SCORE;

                player1Result = LOVE_SCORE;
                score = player1Result + SCORE_SEPARATOR_TOKEN + player2Result;
            }

            bool isPlayer1Winning = player1Points > player2Points;
            bool player1DoesNotHaveAdvantage = player1Points < ADVANTAGE_POINTS;
            if (isPlayer1Winning && player1DoesNotHaveAdvantage)
            {
                if (player1Points == THIRTY_POINTS)
                    player1Result = THIRTY_SCORE;
                if (player1Points == FORTY_POINTS)
                    player1Result = FORTY_SCORE;
                if (player2Points == FIFTEEN_POINTS)
                    player2Result = FIFTEEN_SCORE;
                if (player2Points == THIRTY_POINTS)
                    player2Result = THIRTY_SCORE;
                score = player1Result + SCORE_SEPARATOR_TOKEN + player2Result;
            }

            bool isPlayer2Winning = player2Points > player1Points;
            bool player2DoesNotHaveAdvantage = player2Points < ADVANTAGE_POINTS;
            if (isPlayer2Winning && player2DoesNotHaveAdvantage)
            {
                if (player2Points == THIRTY_POINTS)
                    player2Result = THIRTY_SCORE;
                if (player2Points == FORTY_POINTS)
                    player2Result = FORTY_SCORE;
                if (player1Points == FIFTEEN_POINTS)
                    player1Result = FIFTEEN_SCORE;
                if (player1Points == THIRTY_POINTS)
                    player1Result = THIRTY_SCORE;
                score = player1Result + SCORE_SEPARATOR_TOKEN + player2Result;
            }

            bool player1HasAdvantage = isPlayer1Winning && player2Points >= FORTY_POINTS;
            if (player1HasAdvantage)
            {
                score = PLAYER1_ADVANTAGE_SCORE;
            }

            bool player2HasAdvantage = isPlayer2Winning && player1Points >= FORTY_POINTS;
            if (player2HasAdvantage)
            {
                score = PLAYER2_ADVANTAGE_SCORE;
            }

            if (player1Points >= 4 && player2Points >= 0 && (player1Points - player2Points) >= 2)
            {
                score = "Win for player1";
            }
            if (player2Points >= 4 && player1Points >= 0 && (player2Points - player1Points) >= 2)
            {
                score = "Win for player2";
            }
            return score;
        }

        public void SetP1Score(int number)
        {
            for (int i = 0; i < number; i++)
            {
                P1Score();
            }
        }

        public void SetP2Score(int number)
        {
            for (var i = 0; i < number; i++)
            {
                P2Score();
            }
        }

        private void P1Score()
        {
            player1Points++;
        }

        private void P2Score()
        {
            player2Points++;
        }

        public void WonPoint(string player)
        {
            if (player == "player1")
                P1Score();
            else
                P2Score();
        }

    }
}

