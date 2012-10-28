#include "pch.h"
#include "DirtyAsteriskConverter.h"

using namespace EditRoom;

static Platform::String^ asterisk("*");

DirtyAsteriskConverter::DirtyAsteriskConverter(void)
{
}


DirtyAsteriskConverter::~DirtyAsteriskConverter(void)
{
}

Platform::Object^ DirtyAsteriskConverter::Convert(Platform::Object^ value, Windows::UI::Xaml::Interop::TypeName targetType, Platform::Object^ parameter, Platform::String^ language)
{
	if(static_cast<Platform::Boolean>(value))
	{
		return asterisk;
	}
	return "";
}

Platform::Object^ DirtyAsteriskConverter::ConvertBack(Platform::Object^ value, Windows::UI::Xaml::Interop::TypeName targetType, Platform::Object^ parameter, Platform::String^ language)
{
	return static_cast<Platform::String^>(value) == asterisk;
}

