# Azure Resource Manager Emulator For Compute

## Getting Started

### Create your first Virtual Machine in 5 mins

## Supported APIs
	- Images
		[X] Create Or Update
		[X] Get
		[X] List
	- Galleries
		[X] Create Or Update Gallery
		[X] Get Gallery
	- Gallery Images
		[X] Create Or Update Gallery Image
		[X] Get Gallery Image
	- Gallery Image Versions
		[X] Create Or Update Gallery Image Version
		[X] Get Gallery Image Version
		[] List Gallery Image Versions
	- Virtual Machines
		[] Create Or Update
		[] Start
		[] Power Off
		[] Deallocate (Hibernate)
		[] Get Virtual Machine
	- NetWork Interfaces
		[] Create a Virtual Network
		[] Create a Public IP Address
		[] Create a Network Interface
	- Disks
		[] Create Or Update a Disk
		[] Get a Disk

## Todos
	- NSwag
		- Implement custom generation logic to reduce manual corrections
		- Generated Controllers should return IActionResult to support more status codes
	- Service
		- Create a base service class to reduce boilplate code
	- API
		- Support more error scenarios
	- UX
		- Scala UI
	- Documentation
		- github pages