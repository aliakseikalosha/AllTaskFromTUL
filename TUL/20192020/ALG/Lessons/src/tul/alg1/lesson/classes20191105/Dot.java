package tul.alg1.lesson.classes20191105;

public class Dot {
    public double x;
    public double y;
    public static int dotCount = 0;

    public Dot() {
        dotCount++;
    }

    public Dot(double x, double y) {
        this.x = x;
        this.y = y;
        dotCount++;
    }

    public double getDistance() {
        return Math.sqrt(x * x + y * y);
    }

    public double getDistance(Dot b) {
        return getDistance(this, b);
    }

    public static double getDistance(Dot a, Dot b) {
        return Math.sqrt(Math.pow(a.x - b.x, 2) + Math.pow(a.y - b.y, 2));
    }

    @Override
    protected void finalize() throws Throwable {
        super.finalize();
        dotCount--;
    }

    @Override
    public String toString() {
        return "[" + x + "," + y + "]";
    }
}
