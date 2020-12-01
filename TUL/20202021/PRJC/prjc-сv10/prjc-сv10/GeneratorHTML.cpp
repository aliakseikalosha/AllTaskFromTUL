#include "GeneratorHTML.h"

HTMLBlock::HTMLBlock()
{
}

HTMLBlock::~HTMLBlock()
{
}

std::string Input::print()
{
    return std::string("<div>\n\t<label for=\"username\">Username:</label>\n\t<input type=\"text\" id=\"username\" name=\"username\">\n</div>");
}

std::string Password::print()
{
    return std::string("<div>\n\t<label for = \"pass\"> Password(8 characters minimum) :</label> \n\t<input type = \"password\" id = \"pass\" name = \"password\" minlength = \"8\" required>\n </div>");
}

std::string Checkbox::print()
{
    return std::string("<div>\n\t <input type=\"checkbox\" id=\"scales\" name=\"scales\"checked>\n\t<label for=\"scales\">Scales</label>\n</div>");
}
