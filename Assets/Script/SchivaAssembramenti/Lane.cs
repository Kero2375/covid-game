/**
 * Classe responsabile della gestione delle corsie in cui si può muovere un oggetto
 * */
public class Lane {
    public const float COORD = 3.2F;
    private int currentIndex;
    private float[] lanes = { -COORD, 0, COORD };

    public Lane(int initial = 0) {
        currentIndex = initial;
    }

    public bool ToLeft() {
        if (currentIndex != 0) {
            currentIndex--;
            return true;
        }
        return false;
    }

    public bool ToRight() {
        if (currentIndex != 2) {
            currentIndex++;
            return true;
        }
        return false;
    }

    public float GetLane() {
        return lanes[currentIndex];
    }

    public int Index() {
        return currentIndex;
    }
}
