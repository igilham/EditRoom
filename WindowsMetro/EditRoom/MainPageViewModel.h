#pragma once

using namespace Platform;
using namespace Windows::UI::Xaml::Data;

namespace EditRoom
{

[Bindable]
public ref class MainPageViewModel sealed : public INotifyPropertyChanged
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

	property String^ CurrentFilePath
	{
		String^ get();
		void set(String^ value);
	}

	property String^ Text
	{
		String^ get();
		void set(String^ value);
	}

	virtual event PropertyChangedEventHandler^ PropertyChanged;

private:
	void NotifyPropertyChanged(String^ prop);
	void WriteFile();

	bool dirty;
	String^ currentFilePath;
	String^ text;
};

}

