#pragma once

using namespace Platform;
using namespace Windows::UI::Xaml::Data;
using namespace Windows::UI::Xaml::Interop;

namespace EditRoom
{

public ref class DirtyAsteriskConverter sealed : public IValueConverter
{
public:
	virtual Object^ Convert(Object^ value, TypeName targetType, Object^ parameter, String^ language);
	virtual Object^ ConvertBack(Object^ value, TypeName targetType, Object^ parameter, String^ language);
};

}
