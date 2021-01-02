package tul.alg1.lesson;

import java.util.Scanner;

public class Calculator {

    private static Scanner sc = new Scanner(System.in);

    public static void main(String[] args) {
        System.out.println("Zadej arifmetycky vyraz ve tvaru : a -+/* b");
        double a = sc.nextDouble();
        char opNew, opOld;
        char opCurrent = opOld = sc.next().charAt(0);

        do {
            double b = sc.nextDouble();
            opNew = sc.next().charAt(0);
            if (isMult(opNew)) {
                opOld = opCurrent;
            }
            while (isMult(opNew)) {
                opCurrent = opNew;
                if (isMult(opCurrent)) {
                    double c = sc.nextDouble();
                    b = calculate(b, opCurrent, c);
                }
                opNew = sc.next().charAt(0);
            }
            opCurrent = opOld;
            a = calculate(a, opCurrent, b);
            opCurrent = opOld = opNew;
        } while (opCurrent != '=');
        System.out.println(a);
    }

    private static boolean isMult(char op) {
        return op == '*' || op == '/';
    }

    private static double calculate(double a, char op, double b) {
        switch (op) {
            case '*':
                a *= b;
                break;
            case '-':
                a -= b;
                break;
            case '+':
                a += b;
                break;
            case '/':
                a /= b;
                break;
            default:
                a = Double.NaN;
        }

        return a;
    }
}
