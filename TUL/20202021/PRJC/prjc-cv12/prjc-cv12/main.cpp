#define _CRT_SECURE_NO_WARNINGS
#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include "downloader.h"
#include "fileSave.h"
#include <map>
#include <algorithm>
#include <vector>
#include <cmath>

std::string removeTag(std::string html) {
	std::map<std::string, int> tags;
	std::string tag;
	while (html.find("<") != std::string::npos)
	{
		auto startpos = html.find("<");
		auto endpos = html.find(">") + 1;

		if (endpos != std::string::npos)
		{
			tag = html.substr(startpos, endpos - startpos);
			if (tag[1] == '/') {
				tag = '<' + tag.substr(2, tag.length());
			}
			auto tpos = tag.find(" ");
			if (tpos != std::string::npos) {
				tag = tag.substr(0, tpos) + '>';
			}
			std::map<std::string, int>::iterator i = tags.find(tag);
			if (i != tags.end()) {
				i->second++;
			}
			else {
				tags.insert(std::pair<std::string, int>(tag, 1));
			}

			html.erase(startpos, endpos - startpos);
		}
	}


	std::cout << "All HTML tags in order :" << std::endl;
	using pair_type = decltype(tags)::value_type;
	while (tags.size() > 0) {
		auto pr = std::max_element
		(
			std::begin(tags), std::end(tags),
			[](const pair_type& p1, const pair_type& p2) {
				return p1.second < p2.second;
			}
		);
		std::cout  << pr->first << "  " << pr->second << std::endl;
		tags.erase(pr->first);
	}
	return html;
}

int main()
{
	std::string html;

	html = removeTag(get_Website((char*)"www.example.com"));

	IFileSave* fs = new PDFFileSave();

	fs->save("website", html);

	delete fs;

	return 0;
}
