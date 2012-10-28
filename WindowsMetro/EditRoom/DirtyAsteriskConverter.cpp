#include "pch.h"
#include "DirtyAsteriskConverter.h"

using namespace EditRoom;
using namespace Platform;
using namespace Windows::UI::Xaml::Data;
using namespace Windows::UI::Xaml::Interop;

static Platform::String^ asterisk("*");

Platform::Object^ DirtyAsteriskConverter::Convert(Object^ value, TypeName targetType, Object^ parameter, String^ language)
{
	if(static_cast<bool>(value))
	{
		return asterisk;
	}
	return "";
}

Platform::Object^ DirtyAsteriskConverter::ConvertBack(Object^ value, TypeName targetType, Object^ parameter, String^ language)
{
	return static_cast<String^>(value) == asterisk;
}

