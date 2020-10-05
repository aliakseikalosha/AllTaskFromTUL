package tul.alg1.lesson;

public class Lesson20191015 {

    public static void main(String[] args) {

        {
            int a = 0;
            a++;
        }//prukazevy block

        if (5 == 7) {//podminka libovolny průkaz java který po vyhodnocení ma hodnotu boolean
            System.out.println("5 je stejne 7");
        } else {
            System.out.println("5 neni stejne 7");
        }
        {
            //použití logických pšikayů, podmáněného výrazu
            int a = 10 == 1 ? 1 : 10;//ternární operátor
            int b = 2;
            int max;
            if (a > b) {
                max = a;
            } else {
                max = b;
            }
            if (a > b) max = a;
            else max = b;

            max = (a > b) ? a : b;

            max = Math.max(a, b);
        }
        {
            //proč je lepěí použivat zavorky
            if (true)
                if (false) System.out.println("if");
                else System.out.println("else");

            if (true) {
                if (false) {
                    System.out.println("if");
                }
            } else System.out.println("else");

            double x = -1 + 2 * Math.random();
            double y = -1 + 2 * Math.random();
            double kv;
            if (y >= 0) {
                if (x >= 0) {
                    kv = 1;
                } else {
                    kv = 2;
                }
            } else {
                if (x >= 0) {
                    kv = 4;
                } else {
                    kv = 3;
                }
            }
            System.out.println("x : " + x + "  y : " + y + " => kv : " + kv);
        }
        {
            double a, b, c;
            a = rnd(1, 10);
            b = rnd(1, 10);
            c = rnd(1, 10);

            if ((a + b) > c && Math.abs(a - b) < c) {
                System.out.println("Lze sestroit trouhelnik se storonamy a : " + a + " b : " + b + " c : " + c);
                if (a == b && b == c) {
                    System.out.println("ravnostranný");
                } else if (a == b || b == c || a == c) {
                    System.out.println("rovnoramenný");
                } else {
                    System.out.println("obecná");
                }

                if (c * c == (a * a + b * b)) {
                    //rovno
                    System.out.println("pravoúhlý");
                } else if (c * c > (a * +b * b)) {
                    System.out.println("tupoúhlý");
                } else {
                    System.out.println("ostroúhlý");
                }
            }
        }
        {//while / do-while
            int a = 0;
            while (a>0){
                System.out.println("To nikdy nevypiše");
            }

            do{
                System.out.println("To vypiše jednou");
            }while (a>0);
        }
        {//for
            for (int i = 0; i < 10; i++) {//for(inicializační_část; podmínka; iterační_čast) { tělo_cyklu}

            }
        }

    }

    private static double rnd(double min, double max) {
        return min + (max - min) * Math.random();
    }
}
