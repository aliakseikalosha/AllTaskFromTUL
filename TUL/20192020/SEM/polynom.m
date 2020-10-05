function y=polynom(x, a)
    y = 0;
    for i = length(a):-1:1
        b = a(i);
        if i <= 1 
            y = y + b;
        else
            y = y + b*(x.^(i - 1));
        end
    end
end