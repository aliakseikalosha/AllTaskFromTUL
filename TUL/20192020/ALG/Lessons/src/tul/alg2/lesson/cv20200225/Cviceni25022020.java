package tul.alg2.lesson.cv20200225;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.Scanner;

public class Cviceni25022020 {


    private static Scanner sc = new Scanner(System.in);

    public static void main(String[] args) throws IOException {
        //maxDistance();
        //maxDistanceDot();
        //maxDistanceDotArrayList();
        CombineString();
    }

    private static void maxDistance() {
        System.out.print("Zadej pocet bodu : ");
        int n = sc.nextInt();
        double[][] dots = new double[n][2];

        System.out.println("Zadej souzadnice bodu: [x y]");
        for (int i = 0; i < n; i++) {
            dots[i][0] = sc.nextDouble();
            dots[i][1] = sc.nextDouble();
        }
        double maxDist = -1;
        int a = -1;
        int b = -1;
        for (int i = 0; i < n; i++) {
            for (int j = i; j < n; j++) {
                double dist = calculateDistance(dots[i], dots[j]);
                if (dist > maxDist) {
                    maxDist = dist;
                    a = i;
                    b = j;
                }
            }
        }
        System.out.println("Nejdelsi vzdalenost maji body : \n\t" + a + ": " + dotToSting(dots[a]) + ", \n\t" + b + ": " + dotToSting(dots[b]) + ".\nVzdalenost je " + maxDist);
    }

    private static String dotToSting(double[] a) {
        return String.format("[%.3f,%.3f]", a[0], a[1]);
    }

    private static double calculateDistance(double[] a, double[] b) {
        double x = a[0] - b[0];
        double y = a[1] - b[1];
        return Math.hypot(x, y);
    }

    private static void maxDistanceDot() {
        System.out.print("Zadej pocet bodu : ");
        int n = sc.nextInt();
        Dot[] dots = new Dot[n];

        System.out.println("Zadej souzadnice bodu: [x y]");
        for (int i = 0; i < n; i++) {
            dots[i] = new Dot(sc.nextDouble(), sc.nextDouble());
        }
        double maxDist = -1;
        int a = -1;
        int b = -1;
        for (int i = 0; i < n; i++) {
            for (int j = i; j < n; j++) {
                double dist = dots[i].distanceFrom(dots[j]);
                if (dist > maxDist) {
                    maxDist = dist;
                    a = i;
                    b = j;
                }
            }
        }
        System.out.println("Nejdelsi vzdalenost maji body : \n\t" + a + ": " + dots[a].toString() + ", \n\t" + b + ": " + dots[b].toString() + ".\nVzdalenost je " + maxDist);
    }

    private static void maxDistanceDotArrayList(){

        System.out.print("Zadej pocet bodu : ");
        int n = sc.nextInt();
        ArrayList<Dot> dots = new ArrayList<>();

        System.out.println("Zadej souzadnice bodu: [x y]");
        for (int i = 0; i < n; i++) {
            dots.add( new Dot(sc.nextDouble(), sc.nextDouble()));
        }
        double maxDist = -1;
        int a = -1;
        int b = -1;
        for (int i = 0; i < n; i++) {
            for (int j = i; j < n; j++) {
                double dist = dots.get(i).distanceFrom(dots.get(j));
                if (dist > maxDist) {
                    maxDist = dist;
                    a = i;
                    b = j;
                }
            }
        }
        Iterator it = dots.iterator();
        while(it.hasNext()){
            System.out.println(it.next().toString());
        }
        System.out.println("Nejdelsi vzdalenost maji body : \n\t" + a + ": " + dots.get(a).toString() + ", \n\t" + b + ": " + dots.get(b).toString() + ".\nVzdalenost je " + maxDist);
    }

    private static void CombineString(){
        String s  = "";
        StringBuilder all = new StringBuilder();
        while((s=sc.nextLine()).length()>0){
            all.append(s);
            all.append("\n");
        }
        System.out.println(all.toString());
    }

}
