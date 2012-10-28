#include "pch.h"
#include "MainPageViewModel.h"

using namespace EditRoom;
using namespace Concurrency;
using namespace Platform;
using namespace Windows::Storage;
using namespace Windows::Storage::Pickers;
using namespace Windows::Storage::Streams;
using namespace Windows::UI::Popups;
using namespace Windows::UI::Xaml::Data;


static String^ defaultText("Edit Room is a very simple text editor for Windows.\n\n" +
            "Keyboard shortcuts:\n\n" +
            "* File Menu:\n" +
            "   - Ctrl+N - New file\n" +
            "   - Ctrl+O - Open file\n" +
            "   - Ctrl+S - Save file\n" +
            "   - Ctrl+W - Exit Edit Room\n\n" +
            "* Edit Menu:\n" +
            "   - Ctrl+X - Cut\n" +
            "   - Ctrl+C - Copy\n" +
            "   - Ctrl+V - Paste\n" +
            "   - Ctrl+Z - Undo\n" +
            "   - Ctrl+Y - Redo\n");


// Default constructor.
MainPageViewModel::MainPageViewModel(void) :
	dirty(false),
	currentFilePath(""),
	text(defaultText)
{
}

// Create a new document.
void MainPageViewModel::New()
{
	Text = "";
	Dirty = false;
}

// Open a file.
void MainPageViewModel::OpenFile()
{
	auto picker = ref new FileOpenPicker();
	picker->SuggestedStartLocation = PickerLocationId::DocumentsLibrary;
	picker->ViewMode = PickerViewMode::List;
	task<StorageFile^>(picker->PickSingleFileAsync()).then([this](StorageFile^ file)
	{
		if (file)
		{
			task<String^>(PathIO::ReadTextAsync(file->Path)).then([this](String^ content)
			{
				Text = content;
			});
		}
	});
}

// Helper to ask the user if they would like to save changes in a dirty document before performing a destructive action.
void MainPageViewModel::SaveChanges()
{
	if(Dirty)
	{
		auto dlg = ref new MessageDialog("Would you like to save your changes to this document or discard then?",
			"You have unsaved changes");
		dlg->Commands->Append(ref new UICommand("Save", ref new UICommandInvokedHandler([this](IUICommand^ command)
		{
			SaveFile();
		})));
		dlg->Commands->Append(ref new UICommand("Discard", ref new UICommandInvokedHandler([this](IUICommand^ command)
		{
			// nop
		})));
		dlg->DefaultCommandIndex = 0;
		dlg->CancelCommandIndex = 1;
		dlg->ShowAsync();
	}
}

// Save an open document to disk.
void MainPageViewModel::SaveFile()
{
	if(CurrentFilePath == "")
	{
		// todo: save file
		auto picker = ref new FileSavePicker();
		picker->SuggestedStartLocation = PickerLocationId::DocumentsLibrary;
		picker->DefaultFileExtension = ".txt";
		task<StorageFile^>(picker->PickSaveFileAsync()).then([this](StorageFile^ file)
		{
			if(file)
			{
				CurrentFilePath = file->Path;
				WriteFile();
			}
		});
	}
	else
	{
		WriteFile();
	}
}

// Get the dirty marker.
bool MainPageViewModel::Dirty::get(void)
{
	 return dirty;
}

// Set the dirty marker.
void MainPageViewModel::Dirty::set(bool value)
{
	 if(dirty != value)
	 {
		 dirty = value;
		 NotifyPropertyChanged("Dirty");
	 }
}

// Get the path of the current file.
String^ MainPageViewModel::CurrentFilePath::get(void)
{
	 return currentFilePath;
}

// Set the path of the current file.
void MainPageViewModel::CurrentFilePath::set(String^ value)
{
	 if(currentFilePath != value)
	 {
		 currentFilePath = value;
		 NotifyPropertyChanged("CurrentFilePath");
	 }
}

// Get the current text contents of the open document.
String^ MainPageViewModel::Text::get(void)
{
	 return text;
}

// Set the text content of the open document.
void MainPageViewModel::Text::set(String^ value)
{
	 if(text != value)
	 {
		 text = value;
		 NotifyPropertyChanged("Text");
	 }
}

// private helper to perform the disk write and clean the dirty marker.
void MainPageViewModel::WriteFile()
{
	task<void>(PathIO::WriteTextAsync(CurrentFilePath, Text, UnicodeEncoding::Utf8)).then([this](void)
	{
		Dirty = false;
	});
}

// Property change helper.
void MainPageViewModel::NotifyPropertyChanged(String^ prop)
{
	PropertyChangedEventArgs^ args = ref new PropertyChangedEventArgs(prop);
	PropertyChanged(this, args);
}
