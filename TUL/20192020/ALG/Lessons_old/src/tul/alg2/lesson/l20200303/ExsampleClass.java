package tul.alg2.lesson.l20200303;

public class ExsampleClass {

    public void TestABC() {
        A a = new FinalA();
        A b = new B();
        D c = new C();
        a.Foo();
        b.Foo();
        ((A) c).Foo();
    }

    private ExsampleClass(){ }

    public static ExsampleClass getInstance(){
        return new ExsampleClass();
    }


    private class D {
        protected String name = "D";
    }

    private abstract class A extends D {
        protected int x = 'A';

        public void Foo(){
            name = "A";
        }

        protected void printNameAndX() {
            System.out.println("name : " + name + " , x = " + x);
        }
    }

    private final class FinalA extends A {
        @Override
        public void Foo() {
            printNameAndX();
        }
    }

    private class C extends A/* B nejde poiziti protze b je final*/ {

        @Override
        public void Foo() {
            x = 'C';
            name = "C";
            printNameAndX();
        }

    }

    private final class B extends A {
        @Override
        public void Foo() {
            x = 'B';
            name = "B";
            printNameAndX();
        }
    }
}
