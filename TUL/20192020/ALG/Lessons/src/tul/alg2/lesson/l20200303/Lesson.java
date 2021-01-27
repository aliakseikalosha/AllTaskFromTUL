package tul.alg2.lesson.l20200303;

import java.io.IOException;
import java.util.Random;

public class Lesson {

    public static void main(String[] args) throws IOException {
        String a;
        //priradili a otkaz na " "
        a = " ";
        //priradili a odkaz na null
        a = null;

        Random rnd = new Random();

        ExsampleClass exsample = ExsampleClass.getInstance();
        exsample.TestABC();
    }
}
