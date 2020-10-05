package tul.alg1.lesson;

import tul.alg1.lesson.guessGame.GuessNumberGame;

import java.util.Scanner;

import static tul.alg1.lesson.tools.NaturalNumbersTools.numberOfDeviders;

public class Cviceni20191114 {

    private static Scanner sc = new Scanner(System.in);

    public static void main(String[] args) {
        //Calculator();
        //NumberInfo();
        GuessNumberGame game = new GuessNumberGame();
        game.main(args);
    }

    private static void Calculator() {

        System.out.println("Zadej arifmetycky vyraz ve tvaru : a -+/* b");
        double a = sc.nextDouble();
        char op = sc.next().charAt(0);
        do {
            //while op != '='{while op == '+-'{while op == '*/'}} //todo Homework
            // add method multiplication not while loop
            double b = sc.nextDouble();
            op = sc.next().charAt(0);
            while (op == '+' || op == '-') {
                while (op == '*' || op == '/') {
                    double c = sc.nextDouble();
                    op = sc.next().charAt(0);
                    cal(b, op, c);
                }
            }
            cal(a, op, b);
        } while (op != '=');
        System.out.println(a);
    }

    private static double cal(double a, char op, double b) {
        switch (op) {
            case '+':
                a += b;
                break;
            case '-':
                a -= b;
                break;
            case '*':
                a *= b;
                break;
            case '/':
                a /= b;
                break;
            default:
                a = Double.NaN;
                System.out.println("Chybna zadana operace");
        }
        return a;
    }

    private static void NumberInfo() {
        System.out.println("Zadej cisla : ");
        int number;
        while ((number = sc.nextInt()) > 0) {
            int pd = numberOfDeviders(number);
            System.out.println(pd + " ");
        }
    }


}
