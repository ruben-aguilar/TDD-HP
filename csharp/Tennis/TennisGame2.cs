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
        private int p1point;
        private int p2point;

        private string p1res = "";
        private string p2res = "";
        private string player1Name;
        private string player2Name;

        public TennisGame2(string player1Name, string player2Name)
        {
            this.player1Name = player1Name;
            p1point = 0;
            this.player2Name = player2Name;
        }

        public string GetScore()
        {
            var score = EMPTY_SCORE;
            bool tiedBelowFourtyPoints = (p1point == p2point) && (p1point < FORTY_POINTS);

            if (tiedBelowFourtyPoints)
            {
                if (p1point == ZERO_POINTS)
                    score = LOVE_SCORE;
                if (p1point == FIFTEEN_POINTS)
                    score = FIFTEEN_SCORE;
                if (p1point == THIRTY_POINTS)
                    score = THIRTY_SCORE;
                score += DRAW_SCORE_POSTFIX;
            }

            bool tiedAboveThirtyPoints = (p1point == p2point) && (p1point > THIRTY_POINTS);
            if (tiedAboveThirtyPoints)
                score = DEUCE_SCORE;

            bool player1HasPoints = p1point > ZERO_POINTS;
            bool player2HasNoPoints = p2point == ZERO_POINTS;
            if (player1HasPoints && player2HasNoPoints)
            {
                if (p1point == FIFTEEN_POINTS)
                    p1res = FIFTEEN_SCORE;
                if (p1point == THIRTY_POINTS)
                    p1res = THIRTY_SCORE;
                if (p1point == FORTY_POINTS)
                    p1res = FORTY_SCORE;

                p2res = LOVE_SCORE;
                score = p1res + SCORE_SEPARATOR_TOKEN + p2res;
            }

            if (p2point > 0 && p1point == 0)
            {
                if (p2point == 1)
                    p2res = FIFTEEN_SCORE;
                if (p2point == 2)
                    p2res = THIRTY_SCORE;
                if (p2point == 3)
                    p2res = "Forty";

                p1res = LOVE_SCORE;
                score = p1res + "-" + p2res;
            }

            if (p1point > p2point && p1point < 4)
            {
                if (p1point == 2)
                    p1res = THIRTY_SCORE;
                if (p1point == 3)
                    p1res = "Forty";
                if (p2point == 1)
                    p2res = FIFTEEN_SCORE;
                if (p2point == 2)
                    p2res = THIRTY_SCORE;
                score = p1res + "-" + p2res;
            }
            if (p2point > p1point && p2point < 4)
            {
                if (p2point == 2)
                    p2res = THIRTY_SCORE;
                if (p2point == 3)
                    p2res = "Forty";
                if (p1point == 1)
                    p1res = FIFTEEN_SCORE;
                if (p1point == 2)
                    p1res = THIRTY_SCORE;
                score = p1res + "-" + p2res;
            }

            if (p1point > p2point && p2point >= 3)
            {
                score = "Advantage player1";
            }

            if (p2point > p1point && p1point >= 3)
            {
                score = "Advantage player2";
            }

            if (p1point >= 4 && p2point >= 0 && (p1point - p2point) >= 2)
            {
                score = "Win for player1";
            }
            if (p2point >= 4 && p1point >= 0 && (p2point - p1point) >= 2)
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
            p1point++;
        }

        private void P2Score()
        {
            p2point++;
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

