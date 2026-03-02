using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using LabManager.Desktop.Models;

namespace LabManager.Desktop.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private string _token = string.Empty;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://localhost:5000/api/");
    }

    public void SetToken(string token)
    {
        _token = token;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
    }

    public bool IsAuthenticated => !string.IsNullOrEmpty(_token);

    public async Task<LoginResponse?> LoginAsync(string email, string password)
    {
        var request = new LoginRequest { Email = email, Password = password };
        var response = await _httpClient.PostAsJsonAsync("Auth/login", request);

        if (response.IsSuccessStatusCode)
        {
            var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>(
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            
            if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.Token))
            {
                SetToken(loginResponse.Token);
                return loginResponse;
            }
        }
        return null;
    }

    public async Task<List<PatientModel>> GetPatientsAsync()
    {
        if (!IsAuthenticated) return new List<PatientModel>();
        
        var response = await _httpClient.GetAsync("Patient");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<PatientModel>>(
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<PatientModel>();
        }
        return new List<PatientModel>();
    }

    public async Task<PatientModel?> CreatePatientAsync(CreatePatientModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("Patient", model);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<PatientModel>(
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        return null;
    }
    
    public async Task<List<TestOrderModel>> GetTestOrdersAsync()
    {
        if (!IsAuthenticated) return new List<TestOrderModel>();
        
        var response = await _httpClient.GetAsync("TestOrder");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<TestOrderModel>>(
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<TestOrderModel>();
        }
        return new List<TestOrderModel>();
    }

    public async Task<TestOrderModel?> CreateTestOrderAsync(CreateTestOrderModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("TestOrder", model);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<TestOrderModel>(
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        return null;
    }
}
