#pragma once

namespace EditRoom
{

[Windows::UI::Xaml::Data::Bindable]
public ref class MainPageViewModel sealed : public Windows::UI::Xaml::Data::INotifyPropertyChanged
{
public:
	MainPageViewModel(void);

	void New();
	void OpenFile();
	void SaveChanges();
	void SaveFile();

	property bool Dirty
	{
		bool get();
		void set(bool value);
	}

	property Platform::String^ CurrentFilePath
	{
		Platform::String^ get();
		void set(Platform::String^ value);
	}

	property Platform::String^ Text
	{
		Platform::String^ get();
		void set(Platform::String^ value);
	}

	virtual event Windows::UI::Xaml::Data::PropertyChangedEventHandler^ PropertyChanged;

private:
	void NotifyPropertyChanged(Platform::String^ prop);
	void WriteFile();

	bool dirty;
	Platform::String^ currentFilePath;
	Platform::String^ text;
};

}

