namespace WayToEarth.StaysOfWork
{
    class PlayingResult
    {
        public bool isWin;
        public int valueOfStars;

        public PlayingResult()
        {
            isWin = false;
            valueOfStars = 0;
        }

        public PlayingResult(bool win, int stars)
        {
            isWin = win;
            valueOfStars = stars;
        }
    }
}
