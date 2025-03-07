# Azure Resource Manager Emulator For Compute

## Getting Started

Generate certificate and configure local machine:
```
dotnet dev-certs https -ep ".\https\aspnetapp.pfx" -p password
dotnet dev-certs https --trust
```

Start ARMulator via docker compose:
```
docker compose up
```

### Create your first Virtual Machine in 5 mins

#### 1. Create a new Gallery

```
PUT https://localhost:8000/subscriptions/{subscription-id}/resourceGroups/myResourceGroup/providers/Microsoft.Compute/galleries/myGalleryName?api-version=2024-03-03

{
  "location": "West US",
  "properties": {
    "description": "This is the gallery description."
  }
}
```

#### 2. Create a simple Gallery Image
```
PUT https://localhost:8000/subscriptions/{subscription-id}/resourceGroups/myResourceGroup/providers/Microsoft.Compute/galleries/myGalleryName/images/myGalleryImageName?api-version=2024-03-03

{
  "location": "West US",
  "properties": {
    "osType": "Windows",
    "osState": "Generalized",
    "hyperVGeneration": "V1",
    "identifier": {
      "publisher": "myPublisherName",
      "offer": "myOfferName",
      "sku": "mySkuName"
    }
  }
}
```

#### 3. Create a new Network Interface
```
PUT https://localhost8000/subscriptions/subid/resourceGroups/rg1/providers/Microsoft.Network/networkInterfaces/test-nic?api-version=2024-05-01

{
  "properties": {
    "enableAcceleratedNetworking": true,
    "disableTcpStateTracking": true,
    "ipConfigurations": [
      {
        "name": "ipconfig1",
        "properties": {
          "publicIPAddress": {
            "id": "/subscriptions/subid/resourceGroups/rg1/providers/Microsoft.Network/publicIPAddresses/test-ip"
          },
          "subnet": {
            "id": "/subscriptions/subid/resourceGroups/rg1/providers/Microsoft.Network/virtualNetworks/rg1-vnet/subnets/default"
          }
        }
      },
      {
        "name": "ipconfig2",
        "properties": {
          "privateIPAddressPrefixLength": 28
        }
      }
    ]
  },
  "location": "eastus"
}
```

#### 4. Create a new Virtual Machine
```
PUT https://localhost:8000/subscriptions/{subscription-id}/resourceGroups/myResourceGroup/providers/Microsoft.Compute/virtualMachines/myVM?api-version=2024-07-01

{
  "location": "westus",
  "properties": {
    "hardwareProfile": {
      "vmSize": "Standard_D1_v2"
    },
    "storageProfile": {
      "imageReference": {
        "id": "/subscriptions/{subscription-id}/resourceGroups/myResourceGroup/providers/Microsoft.Compute/images/{existing-custom-image-name}"
      },
      "osDisk": {
        "caching": "ReadWrite",
        "managedDisk": {
          "storageAccountType": "Standard_LRS"
        },
        "name": "myVMosdisk",
        "createOption": "FromImage"
      }
    },
    "osProfile": {
      "adminUsername": "{your-username}",
      "computerName": "myVM",
      "adminPassword": "{your-password}"
    },
    "networkProfile": {
      "networkInterfaces": [
        {
          "id": "/subscriptions/{subscription-id}/resourceGroups/myResourceGroup/providers/Microsoft.Network/networkInterfaces/{existing-nic-name}",
          "properties": {
            "primary": true
          }
        }
      ]
    }
  }
}
```

5. Get the newly created Virtual Machine
```
GET https://localhost:8000/subscriptions/{subscription-id}/resourceGroups/myResourceGroup/providers/Microsoft.Compute/virtualMachines/myVM?$expand=userData&api-version=2024-07-01
```

You have just successfully created your first Virtual Machine!

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
		[x] Create Or Update
		[] Start
		[] Power Off
		[] Deallocate (Hibernate)
		[x] Get Virtual Machine
	- NetWork Interfaces
		[] Create a Virtual Network
		[] Create a Public IP Address
		[x] Create a Network Interface
	- Disks
		[] Create Or Update a Disk
		[] Get a Disk

## Todos
	- NSwag
		- Implement custom generation logic to reduce manual corrections
		- Generated Controllers should return IActionResult to support more status codes
	- Service
		[x] Create a base service class to reduce boilplate code
	- API
		- Support more error scenarios
        - Using ApiError object for error response instead of the handwritten response
	- UX
		- Scala UI
	- Documentation
		- github pages