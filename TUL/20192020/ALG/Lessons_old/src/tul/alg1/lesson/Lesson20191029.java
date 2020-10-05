package tul.alg1.lesson;

import java.util.Scanner;

public class Lesson20191029 {

    private static Scanner sc = new Scanner(System.in);

    public static void main(String[] args) {
        /*
        {
            {
                System.out.print("Zadej počet čisel :");
                int max = sc.nextInt();
                for (int n = 0; n < max; n++) {
                    System.out.print("Zadej čislo : ");
                    int a = sc.nextInt();
                    //delej neco s cislem
                }
            }
            {
                int a;
                do {
                    System.out.print("Zadej dalši čislo větší než 1 : ");
                    a = sc.nextInt();
                    if (a > 0) {
                        //delej neco s "a"
                    }
                }
                while (a > 0);
            }
            {
                int a;
                while ((a = sc.nextInt()) > 0) {

                    //delej neco s "a"
                }
            }
        }//zpracovování dat
        {
            int n = sc.nextInt();
            int min = Integer.MAX_VALUE;
            int max = -Integer.MAX_VALUE;
            for (int i = 0; i < n; i++) {
                int a = sc.nextInt();
                if (max < a) {
                    max = a;
                }
                if(min>a){
                    min = a;
                }
            }
        }//Hledaní min max
         */

        boolean b = true;
        System.out.println(b);
        b&=false;
        System.out.println(" b &= false; =>  b = "+b);
        b=true;
        b&=true;
        System.out.println(" b |= false; =>  b = "+b);

    }
}
