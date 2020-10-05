package tul.alg1.lesson.classes20191105;

import tul.alg1.lesson.classes20191105.inner.InnerPackageClass;

import java.util.Scanner;

public class Lesson20191105 {

    private static Scanner sc = new Scanner(System.in);

    public static void main(String[] args) throws Throwable {

        InnerClass.PrintText();
        InnerPackageClass ipc = new InnerPackageClass();
        ipc.PrintText();

        Dot a = new Dot();
        a.x = 1;
        a.y = 1.1d;
        Dot b = new Dot(1, 16f);
        System.out.println("a : " + a + "\nb : " + b + "\nDot.dotCount : " + Dot.dotCount);
        System.out.println("a je na vzdalenosti " + a.getDistance() + " od pocatku");
        System.out.println("a je na vzdalenosti " + a.getDistance(new Dot(5, 1)) + " od [5,1]]");
        System.out.println("a je na vzdalenosti " + Dot.getDistance(a, b) + " od b");


        b.finalize();
        System.out.println("a : " + a + "\nDot.dotCount : " + Dot.dotCount);
    }

    private static class InnerClass {

        public static void PrintText() {
            System.out.println(GetText());
        }

        private static String GetText() {
            return "Hi, from inner class";
        }
    }


}
