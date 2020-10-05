function y = Taylor(x,d,x0)
    y = zeros(1,length(x));
    
    for i = 0:(length(d)-1)
        y=y+d(i+1).*(x-x0).^(i)/(factorial(i));
    end
end

