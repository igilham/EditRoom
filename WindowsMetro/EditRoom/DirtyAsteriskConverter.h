#pragma once

namespace EditRoom
{

public ref class DirtyAsteriskConverter sealed : public Windows::UI::Xaml::Data::IValueConverter
{
public:
	DirtyAsteriskConverter(void);
	virtual ~DirtyAsteriskConverter(void);

	virtual Platform::Object^ Convert(Platform::Object^ value, Windows::UI::Xaml::Interop::TypeName targetType, Platform::Object^ parameter, Platform::String^ language);
	virtual Platform::Object^ ConvertBack(Platform::Object^ value, Windows::UI::Xaml::Interop::TypeName targetType, Platform::Object^ parameter, Platform::String^ language);

};

}
