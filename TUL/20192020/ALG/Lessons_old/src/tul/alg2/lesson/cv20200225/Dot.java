package tul.alg2.lesson.cv20200225;

public class Dot {
    public double x;
    public double y;

    public Dot(double x, double y) {
        this.x = x;
        this.y = y;
    }

    public double distanceFromOrigin() {
        return distanceFrom(new Dot(0, 0));
    }

    public double distanceFrom(Dot b) {
        return Math.hypot(b.x - x, b.y - y);
    }

    public String toString(){
        return String.format("[%.3f,%.3f]", x, y);
    }
}
